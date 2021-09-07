function init(){
    console.log("I've been initialized");
    var h1tags = document.getElementsByTagName("h1");
    h1tags[0].onclick = changeColor;
}

function changeColor(){
    this.innerHTML = "Click again";
    
    var color = '#' + Math.floor(Math.random()*16777215).toString(16);
    
    this.style.color = color;
}

function toggleImg(){
    console.log("I've been toggled");
    var img = document.getElementById("logoimg");
    var isImgVisible = img.style.visibility != "hidden";
    console.log(isImgVisible);
    img.style.visibility = isImgVisible ? "hidden" : "visible";
}

window.onload = init;