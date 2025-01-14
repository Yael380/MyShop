let passwordScore=0;
const toSignIn = () => {
    const signIn = document.querySelector(".signIn");
    signIn.classList.remove("signIn")
}

const getDataFromFormSignIn = () => {   
    const userName = document.getElementById("userName").value;
    const password = document.getElementById("password").value;
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    if (userName.indexOf('@') == -1)
        alert("Field email must include @")
    else if (passwordScore < 3) 
        alert("weak password")
    else if (firstName.length < 2 || firstName.length > 20 || lastName.length < 2 || lastName.length > 20)
        alert("Name can be between 2 till 20 letters")
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
            if (responsePost.status == 204)
                alert("User not found")
            else {
                const dataPost = await responsePost.json();
                console.log('POST Data:', dataPost);
                alert(`hi ${dataPost.firstName}`)
                sessionStorage.setItem("currenUserId", dataPost.id)
                window.location.href="UpdateUser.html"
            }
        }
    }
    catch (err) {
        
    }  
}
const checkPassword = async () => {
    try {
        const password = document.getElementById("password").value;
        const score = document.getElementById("score");
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
                score.value = dataGet;
                passwordScore = dataGet;
            }
        }
    }
    catch (err) {
        alert(err)
    }
}

