"use strict";

var connection = new signalR
    .HubConnectionBuilder()
    .withUrl("http://localhost:11978/SomeHub") // This is the hub URL
    .build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("GetQrCode", (url, data) => {
    document.getElementById("qrUrl").innerHTML = url;
    document.getElementById("qrCode").src = data;
});

connection.on("Login", (token) => {
    //TODO: Use the token to do login and redirect
    alert("Login: " + token);
});

connection.on("ReceiveSomething", (user, message) => {
    // Do whatever you want here

    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + ": \"" + msg + "\"";
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(() => {
    document.getElementById("sendButton").disabled = false;
    console.log("SignalR connection success");
}).catch((err) => {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", (event) => {
    var name = document.getElementById("name").value;

    connection
        .invoke("SendSomethingFromClient", name, "Ping at "+Date.now()) // Trigger a hub method
        .catch((err) => {
            return console.error(err.toString());
        });
    event.preventDefault();
});