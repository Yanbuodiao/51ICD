function sayHello() {
    const compiler = (document.getElementById("compiler") as HTMLInputElement).value;
    const framework = (document.getElementById("framework") as HTMLInputElement).value;
    return `Hello from ${compiler} and ${framework}!`;
} 
let isDone: boolean = false;
let decLiteral: number = 6;
let hexLiteral: number = 0xf00d;
let binaryLiteral: number = 0b1010;
let octalLiteral: number = 0o744;
let aname: string = "bob";
name = "smith";

let bname: string = `Gene`;
let age: number = 37;
let sentence: string = `Hello, my name is ${bname}.

I'll be ${ age + 1} years old next month.`;

let list: number[] = [1, 2, 3];
let list1: Array<number> = [1, 2, 3];

// Declare a tuple type
let x: [string, number];
// Initialize it
x = ['hello', 10]; // OK

console.log(x[0].substr(1)); // OK

function printLabel(labelledObj: { label: string }) {
  console.log(labelledObj.label);
}
let myObj = { size: 10, label: "Size 10 Object" };
printLabel(myObj);




interface LabelledValue {
  label: string;
}

function printLabel1(labelledObj: LabelledValue) {
  console.log(labelledObj.label);
}

let myObj1 = {size: 10, label: "Size 10 Object"};
printLabel(myObj);




interface SquareConfig {
  color?: string;
  width?: number;
}

function createSquare(config: SquareConfig): {color: string; area: number} {
  let newSquare = {color: "white", area: 100};
  if (config.color) {
    newSquare.color = config.color;
  }
  if (config.width) {
    newSquare.area = config.width * config.width;
  }
  return newSquare;
}

let mySquare = createSquare({color: "black"});



interface SquareConfig1 {
  color?: string;
  width?: number;
}

function createSquare1(config: SquareConfig1): { color: string; area: number } {
  let newSquare = {color: "white", area: 100};
  if (config.color) {
    // Error: Property 'collor' does not exist on type 'SquareConfig'
    //newSquare.color = config.collor;  // Type-checker can catch the mistyped name here
  }
  if (config.width) {
    newSquare.area = config.width * config.width;
  }
  return newSquare;
}

let mySquare1 = createSquare1({color: "black"});
// error: 'colour' not expected in type 'SquareConfig'
//let mySquare2 = createSquare1({ colour: "red", width: 100 });
let mySquare2= createSquare1({ colour: "red", width: 100 } as SquareConfig);
let squareOptions = { colour: "red", width: 100 };
let mySquare3 = createSquare1(squareOptions);


interface SearchFunc {
  (source: string, subString: string): boolean;
}
let mySearch: SearchFunc;
mySearch = function(source: string, subString: string) {
  let result = source.search(subString);
  if (result == -1) {
    return false;
  }
  else {
    return true;
  }
}
let mySearch1: SearchFunc;
mySearch1 = function(src: string, sub: string): boolean {
  let result = src.search(sub);
  if (result == -1) {
    return false;
  }
  else {
    return true;
  }
}
let mySearch3: SearchFunc;
mySearch3 = function(src, sub) {
    let result = src.search(sub);
    if (result == -1) {
        return false;
    }
    else {
        return true;
    }
}


interface StringArray {
  [index: number]: string;
}
let myArray: StringArray;
myArray = ["Bob", "Fred"];


interface NumberDictionary {
  [index: string]: number;
  length: number;    // 可以，length是number类型
  //name: string       // 错误，`name`的类型不是索引类型的子类型
}



interface ClockInterface {
    currentTime: Date;
}
class Clock implements ClockInterface {
    currentTime: Date;
    constructor(h: number, m: number) { }
}



interface ClockInterface1 {
    currentTime: Date;
    setTime(d: Date);
}
class Clock1 implements ClockInterface1 {
    currentTime: Date;
    setTime(d: Date) {
        this.currentTime = d;
    }
    constructor(h: number, m: number) { }
}



interface ClockConstructor {
    new (hour: number, minute: number);
}
//class Clock2 implements ClockConstructor {
//    currentTime: Date;
//    constructor(h: number, m: number) { }
//}




interface ClockConstructor2 {
    new (hour: number, minute: number): ClockInterface2;
}
interface ClockInterface2 {
    tick();
}
function createClock(ctor: ClockConstructor2, hour: number, minute: number): ClockInterface2 {
    return new ctor(hour, minute);
}
class DigitalClock implements ClockInterface2 {
    constructor(h: number, m: number) { }
    tick() {
        console.log("beep beep");
    }
}
class AnalogClock implements ClockInterface2 {
    constructor(h: number, m: number) { }
    tick() {
        console.log("tick tock");
    }
}
let digital = createClock(DigitalClock, 12, 17);
let analog = createClock(AnalogClock, 7, 32);




interface Shape {
    color: string;
}
interface Square extends Shape {
    sideLength: number;
}
let square = <Square>{};
square.color = "blue";
square.sideLength = 10;