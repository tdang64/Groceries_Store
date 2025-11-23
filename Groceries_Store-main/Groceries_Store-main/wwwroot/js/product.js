const API_BASE_URL = "https://localhost:7112/api";

// Get categoryId and categoryName from the URL
const params = new URLSearchParams(window.location.search);
const categoryId = parseInt(params.get("categoryId"));
const categoryName = params.get("categoryName");
const searchQuery = params.get("search");

// Update title text based on category or search query
if (searchQuery) {
    document.getElementById("title").textContent = `Results for: "${searchQuery}"`;
} else if (categoryName) {
    document.getElementById("title").textContent = categoryName; // Display category name
} else {
    document.getElementById("title").textContent = "Products";
}

async function loadProducts() {
    const container = document.getElementById("product-list");
    container.innerHTML = `<p>Loading...</p>`;

    try {
        let url = "";

        if (searchQuery) {
            // 🔎 SEARCH ENDPOINT
            url = `${API_BASE_URL}/Products/search/${encodeURIComponent(searchQuery)}`;
        } else if (categoryId) {
            // 📂 CATEGORY ENDPOINT
            url = `${API_BASE_URL}/Products/byCategoryId/${categoryId}`;
        } else {
            container.innerHTML = `<p>No products found.</p>`;
            return;
        }

        const response = await fetch(url);

        if (!response.ok) {
            throw new Error("API error");
        }

        const data = await response.json();

        container.innerHTML = "";

        if (data.length === 0) {
            container.innerHTML = `
                <p style="color:red; font-size:1.2rem;">
                    Sorry, we do not have that item.
                </p>
            `;
            return;
        }

        // Loop through each product and display
        data.forEach(product => {
            container.innerHTML += `
                <div class="product-card">
                    <h3>${product.name}</h3>
                    <p class="price">$${product.price}</p>
                    <p>${(product.description || "").substring(0, 120)}...</p>
                    <a class="see-more" href="/templates/product-details.html?id=${product.id}">See More →</a>
                </div>
            `;
        });

    } catch (err) {
        container.innerHTML = `<p style="color:red;">Failed to load products</p>`;
        console.error(err);
    }
}

loadProducts();
