const OnLoad = () => {
    LoadCart()
}

const GetCart = ()=> {
    let cart = sessionStorage.getItem('cart');
    cart = cart ? JSON.parse(cart) : [];
    return cart;
}
const LoadCart = () => { 
    const products = GetCart();
    CartDesign(products);
    document.querySelector("#itemCount").innerText = products.length;
    document.querySelector("#totalAmount").innerText = totalAmount(products);
}
const CartDesign = (products) => {
    let tmp = document.getElementById("temp-row");
    document.querySelector("tbody").innerText = ''
    products.forEach(product => {
        let cloneProduct = tmp.content.cloneNode(true);
        cloneProduct.querySelector('.image').style.backgroundImage = `url(Images/${product.image})`
        cloneProduct.querySelector(".itemName").innerText = product.name;
        cloneProduct.querySelector(".availabilityColumn").innerText = true;
        cloneProduct.querySelector(".price").innerText = `${product.price}$`;
        cloneProduct.querySelector(".showText").addEventListener('click', () => { removeProduct(product) })
        document.querySelector("tbody").appendChild(cloneProduct)
    })
}
const removeProduct = (product) => {
    let cart = GetCart();
    let flag = false;
    cart=cart.filter(c => {
        if (c.id == product.id && flag === false) {
            flag = true;
            return false;
        }
        else
            return true;
    })
    sessionStorage.setItem('cart', JSON.stringify(cart));
    LoadCart()
}
const totalAmount = (products) => {
    const value = 0;
    return products.reduce((prev, curr) => prev + curr.price, value);
}
const placeOrder = async () => {
    const user = sessionStorage.getItem('currenUserId')
    if (!user) {
        const result = confirm("אינך מחובר, האם תרצה להתחבר?");
        if (result)
            window.location.href = "login.html";
    }
    else {
        try {
            const postData = getDataOrder();
            if (postData) {
                const responsePost = await fetch('api/Orders', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(postData)
                });
                if (!responsePost.ok) {
                    //alert("bad requset")
                    throw new Error(`http error ${responsePost.status}`)
                    //return
                }
                const dataPost = await responsePost.json();
                console.log('POST Data:', dataPost);
                alert(`Your order ${dataPost.id} is sucssesfully`)
            }
            else {
                alert("You don't have nothing in your cart")
            }
        }
        catch (err) {
            alert(err)
        }
        sessionStorage.removeItem('cart');
        LoadCart();
        document.location.href = "/home.html";
    }
}
const getDataOrder = () => {
    const userId = sessionStorage.getItem('currenUserId')
    console.log(userId)
    const cart = GetCart();
    if (cart.length == 0)
        return null;
    let orderItems = [];
    let sum = 0;
    for (let i = 0; i < cart.length; i++) {
        sum += cart[i].price;
        console.log();
        const orderItem = orderItems?.find(oi => oi.productId == cart[i].id);
        if (orderItem) 
            orderItem.quantity++;
        else
            orderItems.push({ productId: cart[i].id, quantity :1});
    }
    //sum = 40;
    return { UserId:userId, OrderItems: orderItems,Sum:sum }
}
