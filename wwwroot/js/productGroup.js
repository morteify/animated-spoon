"use strict";

const productGroupConnection = new signalR.HubConnectionBuilder().withUrl("/productGroup").build();

productGroupConnection.start().then(() => {
    productGroupConnection.invoke("AddToGroup", document.URL).catch((err) => {
        return console.error(err.toString());
    });
}).catch((err) => {
    return console.error(err.toString());
});

productGroupConnection.on("ReceiveProductEntrance", (currentVisitors, group, connectionId) => {
   document.querySelector('#productGroupAnchor').textContent = currentVisitors
  });



