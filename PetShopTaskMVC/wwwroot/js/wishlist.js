const wishlistTableBody = document.getElementById("wishlistTableBody");

function buildWishlist(data) {
    console.log(data);
    wishlistTableBody.innerHTML = "";

    for (let product of data.wishlistItems) {
        console.log(product);

        wishlistTableBody.innerHTML +=
            `  <tr>
                        <td class="py-4">
                            <div class="cart-info d-flex flex-wrap align-items-center ">
                                <div class="col-lg-3">
                                    <div class="card-image">
                                        <img src="images/${product.coverImageUrl}" alt="cloth" class="img-fluid">
                                    </div>
                                </div>
                                <div class="col-lg-9">
                                    <div class="card-detail ps-3">
                                        <h5 class="card-title">
                                            <a href="#" class="text-decoration-none">${product.name}</a>
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td class="py-4 align-middle">
                            <div class="total-price">
                                <span class="secondary-font fw-medium">$ ${product.price}</span>
                            </div>
                        </td>
                        <td class="py-4 align-middle">
                            <div class="total-price">
                                <span class="secondary-font fw-medium">In Stoke</span>
                            </div>
                        </td>
                        <td class="py-4 align-middle">
                            <div class="d-flex align-items-center">
                                <div class="me-4">
                                    <button onclick="addToBasket(${product.id})" class="btn btn-dark p-3 text-uppercase fs-6 btn-rounded-none w-100">
                                        Add to
                                        cart
                                    </button>
                                </div>
                                <div class="cart-remove">
                                    <a onclick="removeFromWishlist(${product.id})">
                                        <svg width="24" height="24">
                                            <use xlink:href="#trash"></use>
                                        </svg>
                                    </a>
                                </div>
                            </div>
                        </td>
                    </tr>`
    }
}

function initWishlist() {
    fetch(`https://localhost:7045/wishlist/initwishlist`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            buildWishlist(data);
        })
}

function addToWishlist(id) {
    fetch(`https://localhost:7045/wishlist/addtowishlist/${id}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
        });
}

function removeFromWishlist(id) {
    fetch(`https://localhost:7045/wishlist/removefromwishlist/${id}`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            buildWishlist(data);
        });
}
