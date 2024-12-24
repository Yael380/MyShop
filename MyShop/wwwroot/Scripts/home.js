const GetProducts = async ()=>{
    try {
        const responseGet = await fetch('api/Products', {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
        });
        if (!responseGet.ok) {
                //alert("bad requset")
                throw new Error(`http error ${responsePost.status}`)
            }
        const dataGet = await responseGet.json();
        console.log('POST Data:', dataGet);
        return dataGet;
        }
    catch (err) {
        alert(err)
    }

}
const LoadProducts = () => {
    const products=GetProducts()
}