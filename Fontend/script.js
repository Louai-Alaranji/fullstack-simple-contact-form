var form = document.querySelector(".js-form")

form.addEventListener("submit", function(event){
    event.preventDefault();

    var name = document.querySelector(".js-name").value;
    var email = document.querySelector(".js-email").value;
    var phone = document.querySelector(".js-phone").value;
    var adress = document.querySelector(".js-adress").value;

    const contact = {
        FullName: name,
        Email: email,
        Phone: phone,
        Adress: adress
    }
    fetch("https://localhost:7228/api/Contacts", {
        method : "POST",
        headers: {
            "Content-Type" : "application/json"
        },
        body: JSON.stringify(contact)
    })
    .then(response =>{
        if(response.ok){
            console.log("Contact added successfully");
            alert("Contact added successfully")
        } else{
            console.error("Failed to ad contact")
        }
    }).catch(error =>{
        console.error("Error:", error)
    })
})