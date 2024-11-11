const getDataFromFormUpdate = () => {
    const userName = document.getElementById("userName").value;
    const password = document.getElementById("password").value;
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    if (!userName || !password || !firstName || !lastName)
        alert("All field are required")
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