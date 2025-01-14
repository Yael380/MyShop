let categoryIds = [];

const OnLoad = () => {
    LoadProducts();
    LoadCategories();
}

const GetProducts = async () => {
    let url = "api/Products/";
    const { nameSearch, minPrice, maxPrice } = getElementToFilter();
    if (nameSearch || minPrice || maxPrice || categoryIds.length > 0)
        url +='?';
    if (nameSearch)
        url += `&desc=${nameSearch}`;
    if (minPrice)
        url += `&minPrice=${minPrice}`;
    if (maxPrice)
        url += `&maxPrice=${maxPrice}`;
    for (let i = 0; i < categoryIds.length; i++)
        url += `&categoryIds=${categoryIds[i]}`;
    try {
        const responseGet = await fetch( url, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            query: { minPrice, maxPrice, categoryIds, desc:nameSearch }
        });
        if (!responseGet.ok) {
            throw new Error(`http error ${responseGet.status}`)
        }
        const dataGet = await responseGet.json();
        console.log(dataGet);
        return dataGet;
        }
    catch (err) {
        console.log(err);
    }
}

const LoadProducts = async () => {
    const products = await GetProducts();
    let tmp = document.getElementById("temp-card");
    document.getElementById("PoductList").innerHTML = '';
    products.forEach(product => {
        let cloneProduct = tmp.content.cloneNode(true);
        cloneProduct.querySelector("img").src = `./Images/${product.image}`
        cloneProduct.querySelector("h1").textContent = product.name;
        cloneProduct.querySelector(".price").innerText = product.price;
        cloneProduct.querySelector(".description").innerText = product.description;
        cloneProduct.querySelector("button").addEventListener('click', () => { addToCart(product) })
        document.getElementById("PoductList").appendChild(cloneProduct)
    })
    const cart = GetCart();
    document.querySelector("#ItemsCountText").innerHTML = cart.length;
}
const GetCategories = async () => {
    try {
        const responseGet = await fetch('api/Categories', {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
        });
        if (!responseGet.ok) {
            throw new Error(`http error ${responseGet.status}`)
        }
        const dataGet = await responseGet.json();
        console.log(dataGet);
        return dataGet;
    }
    catch (err) {
        console.log(err);
    }

}
const LoadCategories = async () => {
    const categories = await GetCategories();
    let tmp = document.getElementById("temp-category");
    categories.forEach(category => {
        let cloneCategory = tmp.content.cloneNode(true);
        cloneCategory.querySelector(".OptionName").textContent = category.name;
        cloneCategory.querySelector(".opt").addEventListener('change', () => { ChangeCategoryArr(category.id) })
        document.getElementById("categoryList").appendChild(cloneCategory)
    })
}
const ChangeCategoryArr = (categoryId) => {
    const id = categoryIds.find(c => c == categoryId)
    if (id) {
        categoryIds = categoryIds.filter(c => c != categoryId)
    }
    else {
        categoryIds.push(categoryId)
    }
    filterProducts();
}

const filterProducts = () => {
    LoadProducts();
}
const getElementToFilter=()=>{
    const nameSearch = document.querySelector("#nameSearch").value;
    const minPrice = parseInt(document.querySelector("#minPrice").value);
    const maxPrice = parseInt(document.querySelector("#maxPrice").value);
    return { nameSearch, minPrice, maxPrice }
}

const addToCart = (product) => {
    sessionStorage.setItem('currenUserId', 2);
    const user = sessionStorage.getItem('currenUserId')
    if (!user) {
        const result = confirm("אינך מחובר, האם תרצה להתחבר?");
        if (result)
            window.location.href = "login.html";
    }
    else {
        cart = GetCart()
        cart.push(product);
        sessionStorage.setItem('cart', JSON.stringify(cart));
        document.querySelector("#ItemsCountText").innerHTML = cart.length;
        //alert('המוצר נוסף בהצלחה');
    }
}
const GetCart = () => {
    let cart = sessionStorage.getItem('cart')
    cart = cart ? JSON.parse(cart) : [];
    return cart;
}
// minPrice, maxPrice, categoryIds, desc 
