﻿const OnLoad = () => {
    LoadCart()
}

const GetCart = ()=> {
    let cart = sessionStorage.getItem('cart');
    cart = cart ? JSON.parse(cart) : [];
    return cart;
}
const LoadCart = () => { 
    const products = GetCart();//divide to 2 funcs- add drawOne func 
    let tmp = document.getElementById("temp-row");
    document.querySelector("tbody").innerText=''
    products.forEach(product => {//map is nicer
        let cloneProduct = tmp.content.cloneNode(true);
        cloneProduct.querySelector('.image').style.backgroundImage = `url(Images/${product.image})`
        cloneProduct.querySelector(".itemName").innerText = product.name;
        //cloneProduct.querySelector(".availabilityColumn").innerText = true;
        cloneProduct.querySelector(".price").innerText = product.price;
        cloneProduct.querySelector(".showText").addEventListener('click', () => { removeProduct(product) })
        document.querySelector("tbody").appendChild(cloneProduct)
    })
    document.querySelector("#itemCount").innerText = products.length;
    document.querySelector("#totalAmount").innerText = totalAmount(products);


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
            alert("Your order is sucssesfully")
        }
    }
    catch (err) {
        alert(err)
    }
    sessionStorage.removeItem('cart');
    LoadCart();
}
const getDataOrder = () => {
    const userId = sessionStorage.getItem('currenUserId')
    console.log(userId)
    const cart = GetCart();
    let orderItems = [];
    for (let i = 0; i < cart.length; i++) {
        const orderItem = orderItems?.find(oi => oi.productId == cart[i].id);
        if (orderItem)
            orderItem.quantity++;
        else
            orderItems.push({ productId: cart[i].id, quantity :1});
    }
    return { UserId:userId, OrderItems: orderItems }
}
//<tr class="item-row">
//    <td class="imageColumn"><a rel="lightbox" href="#"><div class="image"></div></a></td>
//    <td class="descriptionColumn"><div><h3 class="itemName"></h3><h6><p class="itemNumber"></p><a class="viewLink" href="https://www.next.co.il/he/g59522s11#407223">לפרטים נוספים</a></h6></div></td>
//    <td class="availabilityColumn"><div>במלאי</div></td>
//    <td class="totalColumn delete"><div class="expandoHeight" style="height: 99px;"><p class="price"></p><a href="#" title="לחצו כאן כדי להסיר את פריט זה" class="Hide DeleteButton showText">הסרת פריט</a></div></td>
//</tr>