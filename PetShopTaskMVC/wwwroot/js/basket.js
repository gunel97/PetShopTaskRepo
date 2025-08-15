const basketItems = document.getElementById("basketItems");
const basketItemsTotal = document.getElementById("basketItemsTotal");
const basketItemsCount = document.getElementById("basketItemsCount");

function addToBasket(id) {
    fetch(`https://localhost:7045/basket/addtobasket/${id}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            buildCart(data);
        });
}

function buildCart(data) {
    basketItems.innerHTML = "";

    for (let product of data.items) {
        console.log(product);

        basketItems.innerHTML +=
            ` <li class="list-group-item d-flex justify-content-between lh-sm">
                    <div>
                        <h6 class="my-0">${product.name}</h6>
                        <small class="text-body-secondary">Brief description</small>
                        <br/>
                        <small class="text-body-secondary">Quantity: ${product.count}</small>
                    </div>
                    <span class="text-body-secondary">$${product.price}</span>
                </li>`
    }
    basketItems.innerHTML +=
        `<li class="list-group-item d-flex justify-content-between">
                    <span class="fw-bold">Total (USD)</span>
                    <strong id="basketItemsTotal">$ ${data.total}</strong>
                </li>`

    basketItemsCount.innerHTML = data.count;

}