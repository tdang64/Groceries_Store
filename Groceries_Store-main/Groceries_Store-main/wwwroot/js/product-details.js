const API_BASE_URL = "https://localhost:7112/api";

// Read product ID from URL
const params = new URLSearchParams(window.location.search);
const id = parseInt(params.get("id"));

// HTML elements
const nameEl = document.getElementById("product-name");
const priceEl = document.getElementById("product-price");
const descEl = document.getElementById("product-description");

async function loadProduct() {
    nameEl.textContent = "Loading...";

    try {
        const response = await fetch(`${API_BASE_URL}/Products/${id}`);
        if (!response.ok) throw new Error("Product not found");

        const product = await response.json();

        nameEl.textContent = product.name;
        priceEl.textContent = `$${product.price}`;
        descEl.textContent = product.description ?? "No description available";

    } catch (err) {
        nameEl.textContent = "Error loading product";
        console.error(err);
    }
}

loadProduct();
