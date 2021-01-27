"use strict";

const visitorsCounterConnection = new signalR.HubConnectionBuilder().withUrl("/visitorsCounter").build();

visitorsCounterConnection.start().then(() => {
    visitorsCounterConnection.invoke("RegisterVisitorEntrance").catch((err) => {
        return console.error(err.toString());
    });
}).catch((err) => {
    return console.error(err.toString());
});

visitorsCounterConnection.on("ReceiveVisitorEntrance", (currentVisitors) => {
    document.querySelector("#visitorsCounter").textContent = currentVisitors;
  });
