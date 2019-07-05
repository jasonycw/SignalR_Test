"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:11978/SomeHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("GetQrCode", (url, data) => {
    document.getElementById("qrUrl").innerHTML = url;
    document.getElementById("qrCode").src = data;
});

connection.on("ReceiveSomething", (user, message) => {
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
        .invoke("SendSomethingFromClient", name, "Ping at "+Date.now())
        .catch((err) => {
            return console.error(err.toString());
        });
    event.preventDefault();
});