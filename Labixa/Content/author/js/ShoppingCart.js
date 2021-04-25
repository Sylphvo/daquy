let product = [
    {
        name: "Vòng Đá Thạch Anh",
        tag: "vongdathachanh",
        price: 420,
        inCart: 0,
    },
    {
        name: "Vòng Đá",
        tag: "vongda",
        price: 210,
        inCart: 0,
    },
    {
        name: "Triple Seat Sofa",
        tag: "3SeatSofa",
        price: 180,
        inCart: 0,
    },
    {
        name: "Multifunction Bed Red",
        tag: "multifunctionbedred",
        price: 850,
        inCart: 0,
    },
    {
        name: "Minimalist Corner Desk",
        tag: "minimalistcornerdesk",
        price: 240,
        inCart: 0,
    },
    {
        name: "Decorative Fabric Sofa",
        tag: "decorativefabricsofa",
        price: 550,
        inCart: 0,
    },
    {
        name: "Artistic Wood Hanger",
        tag: "artisticwoodhanger",
        price: 120,
        inCart: 0,
    },
    {
        name: "Classic Wood Chair",
        tag: "classicwoodchair",
        price: 170,
        inCart: 0,
    },
    {
        name: "White Blue Bed",
        tag: "whitebluebed",
        price: 650,
        inCart: 0,
    },
    {
        name: "Woven Dinning Chair",
        tag: "wovendinningchair",
        price: 180,
        inCart: 0,
    },
    {
        name: "Classic Colorful Chair",
        tag: "classiccolorfullchair",
        price: 200,
        inCart: 0,
    },
    {
        name: "Rattan Triple Seat Sofa",
        tag: "rattan3seatsofa",
        price: 380,
        inCart: 0,
    },
]


let carts = document.querySelectorAll('.btn-add-cart');
for (let i = 0; i < carts.length; i++) {
    carts[i].addEventListener('click', () => {
        cartNumbers(product[i]);
        totalprice(product[i]);
    })
};
// count Add to Cart
function onloadCartNumbers() {
    let producNum = localStorage.getItem('cartNumbers')
    if (producNum) {
        document.querySelector('.cart .span-number').textContent = producNum
    }
}
function cartNumbers(product) {

    let productNum = localStorage.getItem('cartNumbers');
    productNum = parseInt(productNum);
    if (productNum) {
        localStorage.setItem('cartNumbers', productNum + 1);
        document.querySelector('.cart .span-number').textContent = productNum + 1;
    }
    else {
        localStorage.setItem('cartNumbers', 1);
        document.querySelector('.cart .span-number').textContent = 1;
    }
    setItems(product);
};

// Add items to the cart
function setItems(product) {
    let cartItems = localStorage.getItem('productsInCart');
    cartItems = JSON.parse(cartItems);
    if (cartItems != null) {
        if (cartItems[product.tag] == undefined) {
            cartItems = {
                ...cartItems,
                [product.tag]: product
            }
        }
        cartItems[product.tag].inCart += 1;
    }
    else {
        product.inCart = 1;

        cartItems = {
            [product.tag]: product
        }
    }

    localStorage.setItem("productsInCart", JSON.stringify(cartItems));
}
//Math price
function totalprice(product) {

    let cartPrice = localStorage.getItem('totalprice');

    if (cartPrice != null) {
        cartPrice = parseInt(cartPrice);
        localStorage.setItem("totalprice", cartPrice + product.price);
    }
    else {
        localStorage.setItem("totalprice", product.price);
    }

}
function displayCart() {
    let cartItems = localStorage.getItem('productsInCart');
    cartItems = JSON.parse(cartItems);
    let productTable = document.querySelector(".product-cart-body");
    let basketTable = document.querySelector(".basket")
    if (cartItems && productTable && basketTable) {
        productTable.innerHTML = '';
        basketTable.innerHTML = '';
        Object.values(cartItems).map(item => {
            productTable.innerHTML +=
                '<div class="product-cart-body" style="width:100%;max-width:650px;display:flex;justify-content: flex-start;margin: 0 auto"><div style="width:45%"><img src="~/Content/stone/images/stone/${item.tag}.jpeg"><span>${item.name}</span></div><div class="price" style="width:15%;display:flex;align-items:center">$${item.price},00 </div><div class="quanlity" style="width: 20%; display: flex; align-items: center">${item.inCart}</div ><div class="Total" style="width: 20%; display: flex; align-items: center" >$(${item.price}*${item.inCart})</div > ';
            basketTable.innerHTML ='<div><h3>THÔNG TIN GIỎ HÀNG</h3></div><div><h4>Tổng giá trị đơn hàng<h4><h5>$${cartPrice},00<h5></div>'
        });
    }
    
}
displayCart();
onloadCartNumbers();

