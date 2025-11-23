// Shared API Base URL
const API_BASE_URL = "https://localhost:7112/api";

// Handle search click
document.getElementById("searchBtn")?.addEventListener("click", performSearch);

// Handle Enter key
document.getElementById("searchInput")?.addEventListener("keypress", function (e) {
    if (e.key === "Enter") performSearch();
});

async function performSearch() {
    const query = document.getElementById("searchInput").value.trim();

    if (!query) return;

    // Go to search results page
    window.location.href = `/templates/products.html?search=${encodeURIComponent(query)}`;
}
