let passwordScore = 0;
const getDataFromFormUpdate = () => {
    const userName = document.getElementById("userName").value;
    const password = document.getElementById("password").value;
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    if (!userName || !password || !firstName || !lastName)
        alert("All field are required")
    else if (passwordScore < 3)
        alert("weak passwordl")
    else
        return ({ userName, password, firstName, lastName })
}

const update = async() => {
    try {
        const id = sessionStorage.getItem("currenUserId")
        const putData = getDataFromFormUpdate();
        if (putData) {
            const responsePut = await fetch(`api/Users/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(putData)
            });
            if (!responsePut.ok)
                throw new Error(`http error ${responsePut.status}`)
            alert("Update succeessfully")     
        }
    }
    catch (err) {
        console.log("err")
    }
}
const checkPassword = async () => {
    try {
        const postData = document.getElementById("password").value;
        const score = document.getElementById("score");
        if (postData) {
            const responsePost = await fetch(`api/Users/check?password=${postData}`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                query: { password: postData.password }
            });
            if (!responsePost.ok) {
                throw new Error(`http error ${responsePost.status}`)
            }
            else {
                const dataPost = await responsePost.json();
                console.log('POST Data:', dataPost);
                score.value = dataPost;
                passwordScore = dataPost;
            }
        }
    }
    catch (err) {
        alert(err)
    }
}
