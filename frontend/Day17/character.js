class Character {
    weapon = "";
    armorList = [4];
    race = "";

    vitality = 0;
    strength = 0;
    agility = 0;
    intellect = 0;
    constructor(name) {
        this.name = name;
    }
}

function saveFunc() {
    let name = document.getElementById("name");
    let player = new Character(name);
    
    player.race = document.getElementById("race");
    player.weapon = document.querySelector('input[name="weapon"]:checked').value;
    
    player.vitality  = document.getElementById("vitality0").valueAsNumber;
    player.strength  = document.getElementById("strength1").valueAsNumber;
    player.agility   = document.getElementById("agility2").valueAsNumber;
    player.intellect = document.getElementById("intellect3").valueAsNumber;

    for(i = 0; i <4; i++){
        player.armorList[i] = document.getElementById("armor" + i).value;
    }

    console.log(player);
}


let prevStats = [4];

function statChangeFnc(event){
    let statChanged = event.target.id.toString();
    let statIndex = parseInt(statChanged.substr(-1));

    let newStatValue = event.target.value;
    let valueDiff = prevStats[statIndex] - newStatValue;

    let pointsLeft = parseInt(document.getElementById("pointsLeft").innerHTML);
    if(pointsLeft + valueDiff < 0){
        event.target.value = prevStats[statIndex];
        return;
    }

    prevStats[statIndex] = newStatValue;
    document.getElementById("pointsLeft").innerHTML = pointsLeft + valueDiff;
}

function init(){
    console.log("init");

    prevStats[0] = document.getElementById("vitality0").valueAsNumber;
    prevStats[1] = document.getElementById("strength1").valueAsNumber;
    prevStats[2] = document.getElementById("agility2").valueAsNumber;
    prevStats[3] = document.getElementById("intellect3").valueAsNumber;

    document.getElementById("vitality0").addEventListener("change", statChangeFnc);
    document.getElementById("strength1").addEventListener("change", statChangeFnc);
    document.getElementById("agility2").addEventListener("change", statChangeFnc);
    document.getElementById("intellect3").addEventListener("change", statChangeFnc);
}

function resetFunc(){
    prevStats = [1,1,1,1];
    document.getElementById("pointsLeft").innerHTML = 10;
}

window.onload = init;