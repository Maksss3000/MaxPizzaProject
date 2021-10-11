 "use strict";

document.addEventListener('DOMContentLoaded',()=>{

let images;
images=document.querySelector(".imagess");

let order;
order=document.querySelector(".btn_round");

order.addEventListener('click',orderNow);

// let body=document.querySelector("body");
function orderNow(){
    var op = 0.1;  // initial opacity
    images.style.opacity = op;
    images.style.display = 'flex';
    var timer = setInterval(function () {
        if (op >= 1){
            clearInterval(timer);
        }
        images.style.opacity = op;
        images.style.filter = 'alpha(opacity=' + op * 100 + ")";
        op += op * 0.01;
    }, 10);
    // images.classList.toggle('fade');
   // body.innerHTML=` <h1>Hello</h1>`;

}

// function unfade(element) {
//     var op = 0.1;  // initial opacity
//     element.style.display = 'block';
//     var timer = setInterval(function () {
//         if (op >= 1){
//             clearInterval(timer);
//         }
//         element.style.opacity = op;
//         element.style.filter = 'alpha(opacity=' + op * 100 + ")";
//         op += op * 0.1;
//     }, 10);
// }

});

