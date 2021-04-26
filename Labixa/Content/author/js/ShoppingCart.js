//set product tag for product => set it step by step
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

//Get btn add cart (Please add class for it before start)
let carts = document.querySelectorAll('.btn-add-cart');
//Check button if have and when i click it
for (let i = 0; i < carts.length; i++) {
    carts[i].addEventListener('click', () => {
        cartNumbers(product[i]);
        totalprice(product[i]);
    })
};

function onloadCartNumbers() {
    let producNum = localStorage.getItem('cartNumbers')
    //load page with span number set
    if (producNum) {
        document.querySelector('.cart .span-number').textContent = producNum
    }

}
//Count Add to Cart
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
    //Get cartItems in local Storage
    let cartItems = localStorage.getItem('productsInCart');
    //Because of productsInCart now is string but now we want it string to array Json =>
    cartItems = JSON.parse(cartItems);
    //Check cartItems 
    if (cartItems != null) {
        //Check product have tag or not =>
        if (cartItems[product.tag] == undefined) {
            //Set cartItem with tag =>
            cartItems = {
                ...cartItems,
                [product.tag]: product
            }
        }
        //=> if cartItem in stock => +1 cart in next time
        cartItems[product.tag].inCart += 1;
    }
    else {
        //=>if not, cart =1 and next time will be +1=> loop if
        product.inCart = 1;
        //=>set cartItems
        cartItems = {
            [product.tag]: product
        }
    }
    //cartItems array to string =>
    localStorage.setItem("productsInCart", JSON.stringify(cartItems));
}
//Math price
function totalprice(product) {
    //get Price in local Storage
    let cartPrice = localStorage.getItem('totalprice');
    //Check cartPrice 
    //if want to check type of Cart Price, console.log(typeof cartPrice)
    if (cartPrice != null) {
        //Change cartPrice string to Int
        cartPrice = parseInt(cartPrice);
        //if cartPrice have => cartPrice + price product next if you add to cart
        localStorage.setItem("totalprice", cartPrice + product.price);
    }
    else {
        //if cartPrice don't hvae in cart
        localStorage.setItem("totalprice", product.price);
    }

}
//Display Cart
function displayCart() {
    //get Items in Local Storage (if want debug not failed, f12+ Application and clear Local store and add item in Cửa hàng)
    let cartItems = localStorage.getItem('productsInCart');
    //Get Price in Local Storage
    let cartPrice = localStorage.getItem('totalprice');
    cartItems = JSON.parse(cartItems);
    //Get Table to display product in cart
    let productTable = document.querySelector(".product-cart-body");
    //Get Table to display total Cart 
    let basketTable = document.querySelector(".basket")
    if (cartItems && productTable && basketTable) {
        productTable.innerHTML = '';
        basketTable.innerHTML = '';
        Object.values(cartItems).map(item => {
            productTable.innerHTML +=
                //if want show img, make img.jpg = tag product (tag product in the first line)
                '<div class="product-cart-body" style="width:100%;max-width:650px;display:flex;justify-content: flex-start;margin: 0 auto"><div style="width:45%"><img src="~/Content/stone/images/stone/' + item.tag + '.jpeg"><span>' + item.name + '</span></div><div class="price" style="width:15%;display:flex;align-items:center">$' + item.price + ',00 </div><div class="quanlity" style="width: 20%; display: flex; align-items: center">' + item.inCart + '</div ><div class="Total" style="width: 20%; display: flex; align-items: center" >$' + item.price * item.inCart + '</div > ';
            basketTable.innerHTML = '<div><h3>THÔNG TIN GIỎ HÀNG</h3></div><div><h4>Tổng giá trị đơn hàng<h4><h5>$' + cartPrice + ',00<h5></div>'
        });
    }
    
    
}
displayCart();
onloadCartNumbers();

