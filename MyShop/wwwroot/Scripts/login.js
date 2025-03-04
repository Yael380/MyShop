let passwordScore=0;
const toSignIn = () => {
    document.getElementById('login_div').style.display = 'none'
    document.querySelector('.signIn_div').style.display = 'block';
}

const getDataFromFormSignIn = () => {   
    const userName = document.getElementById("userName").value;
    const password = document.getElementById("password").value;
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    if (userName.indexOf('@') == -1 || userName.indexOf('@') == userName.length-1)
        alert("email is worng")
    else if (passwordScore < 3) 
        alert("weak password")
    else if (firstName.length < 2 || firstName.length > 50 || lastName.length < 2 || lastName.length > 50)
        alert("Name can be between 2 till 50 letters")
    else if (!userName || !password || !firstName || !lastName)
        alert("All field are required")
    else 
        return ({ userName, password, firstName, lastName })
}

const signIn = async () => {
    try {
        const postData = getDataFromFormSignIn();
        if (postData) {
            const responsePost = await fetch('api/Users', {
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
            alert("SignIn succeessfully")
            document.getElementById('login_div').style.display = 'block'
            document.querySelector('.signIn_div').style.display = 'none';
        }
    }
    catch (err) {
        alert(err)
    }
}

const getDataFromFormLogIn = () => {

    const userName = document.getElementById("userNameLogIn").value;
    const password = document.getElementById("passwordLogIn").value;
    if (!userName || !password)
        alert("All field are required")
    else
        return ({ userName, password})
}

const logIn = async () => {
    const postData = getDataFromFormLogIn();
    try {
        if (postData) {
            const responsePost = await fetch(`api/Users/login?userName=${postData.userName}&password=${postData.password}`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(postData),
                query: { userName: postData.userName, password: postData.password }
            });
            if (!responsePost.ok)
                throw new Error(`http error ${responsePost.status}`)
            if (responsePost.status === 204)
                alert("User not found")
            else {
                const dataPost = await responsePost.json();
                console.log('POST Data:', dataPost);
                alert(`hi ${dataPost.firstName}`)
                sessionStorage.setItem("currenUserId", dataPost.id)
                window.location.href = "Home.html"
            }
        }
    }
    catch (err) {
        console.log(err)
    }  
}
const checkPassword = async () => {
    try {
        const password = document.getElementById("password").value;
        if (password) {
            const responseGet = await fetch(`api/Users/check?password=${password}`, {
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                query: { password: password }
            });
            if (!responseGet.ok) {
                throw new Error(`http error ${responsePost.status}`)
            }
            else { 
                const dataGet = await responseGet.json();
                console.log('GET Data:', dataGet);
                const score = document.getElementById("score").value=dataGet;
                console.log(score);
                passwordScore = dataGet;
            }
        }
    }
    catch (err) {
        alert(err)
    }
}

const update = async () => {
    try {
        const id = sessionStorage.getItem("currenUserId")
        const putData = getDataFromFormSignIn();
        if (putData) {
            const responsePut = await fetch(`api/Users/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(putData)
            });
            if (!responsePut.status==400) 
                alert("weak password")
            if (!responsePut.ok)
                throw new Error(`http error ${responsePut.status}`)
            alert("Update succeessfully")
            window.location.href = "Home.html"
        }
    }
    catch (err) {
        console.log(err)
    }

}
const fillfields = (user) =>{
    document.getElementById("userName").value = user.userName;
    //document.getElementById("password").value = '';
    document.getElementById("firstName").value = user.firstName;
    document.getElementById("lastName").value = user.lastName;
}

const OnUpdateLoad = async () => {
    const user = await GetUserById();
    if (user)
        fillfields(user);
    checkPassword()
}

const GetUserById = async () => {
    try {
        const id = sessionStorage.getItem("currenUserId")
        if (id) {
            const responsePost = await fetch(`api/Users/${id}`, {
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
            });
            if (!responsePost.ok) {
                //alert("bad requset")
                throw new Error(`http error ${responsePost.status}`)
                //return
            }
            const data = await responsePost.json();
            console.log(data); // שמירת התגובה במשתנה והדפסתה
            return data;
        }
    }
    catch (err) {
    alert(err)
}
}

