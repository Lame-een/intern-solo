function submitFunc() {
    //var form = document.querySelector("form");
    var form = document.getElementById("POSTform");
    console.log(form.elements);
    var arr = Array.from(form.elements)
    console.log(arr);
    //console.log(arr[0])
    //console.log(arr[0].attributes)
    console.log(arr[0].value)

    document.getElementsByName("history")[0].style.visibility = "hidden";
}

function calcPi(iterations) {
    //const piVal = 3.14159;
    let pi = 0;

    for (let i = 1; i < iterations; i++) {
        if (i % 2 == 1) {
            pi += 4 / (2 * i - 1);
        } else {
            pi -= 4 / (2 * i - 1);
        }
    }

    document.getElementById("output0").value = pi.toFixed(10);

    return pi;
}

let fibList = [1, 1];

function fib(i) {
    if (i >= fibList.length) {
        fibList.push(fib(i - 1) + fib(i - 2))
    }
    return fibList[i];

}

function getFibList(numOfFibs) {
    for (i = 0; i < numOfFibs; i++) {
        fibList[i] = fib(i);
    }

    document.getElementById("output0").value = fibList.toString();

}


//MAD LIB SECTION

let madLibText = "My dear old ~ sat me down to hear some words of wisdom \n 1. Give a man a ~ and you ~ him for a day ~ a man to ~ and he'll ~ forever \n 2. He who ~ at the right time can ~ again \n 3. Always wear ~ ~ in case you're in a ~ \n 4. Don't use your ~ to wipe your ~ Always have a clean ~ with you";

let madLibArray = [];
let inputArray = [];

function createInputArray() {
    for (i = 0; i < 14; i++) {
        inputArray[i] = document.getElementById("i" + i).value;
    }
}

function checkForMissingInput() {
    let defaultArrayVals = ["Person", "Noun", "Verb", "Adjective", "Plural Verb", "Body Part", "Event"];

    for (const element of inputArray) {
        if (defaultArrayVals.includes(element)) {
            return true;
        }
    }

    return false;
}

function createMadLibSentence() {
    let cursor = 0;
    for (i = 0; (i < 14); i++) {
        while ((madLibArray[cursor++] != "~"));
        madLibArray[cursor - 1] = inputArray[i];
    }

    return madLibArray.join(" ");
}

function madLibGen() {
    madLibArray = madLibText.split(" ");
    createInputArray();

    if (checkForMissingInput()) {
        document.getElementById("output0").value = "Not all values have been entered above."
        return;
    }

    document.getElementById("output0").value = createMadLibSentence();
    //document.getElementById("output0").value = madLibSentence;
}

