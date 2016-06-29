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



interface Shape1 {
    color: string;
}
interface PenStroke1 {
    penWidth: number;
}
interface Square1 extends Shape1, PenStroke1 {
    sideLength: number;
}
let square1 = <Square1>{};
square1.color = "blue";
square1.sideLength = 10;
square1.penWidth = 5.0;




interface Counter {
    (start: number): string;
    interval: number;
    reset(): void;
}
function getCounter(): Counter {
    let counter = <Counter>function (start: number) { };
    counter.interval = 123;
    counter.reset = function () { };
    return counter;
}
let c = getCounter();
c(10);
c.reset();
c.interval = 5.0;



class Control {
    private state: any;
}
interface SelectableControl extends Control {
    select(): void;
}
class Button extends Control {
    select() { }
}
class TextBox extends Control {
    select() { }
}
class Image1 extends Control {
}
class Location1 {
    select() { }
}



class Greeter {
    greeting: string;
    constructor(message: string) {
        this.greeting = message;
    }
    greet() {
        return "Hello, " + this.greeting;
    }
}
let greeter = new Greeter("world");




//class Animal {
//    name:string;
//    constructor(theName: string) { this.name = theName; }
//    move(distanceInMeters: number = 0) {
//        console.log(`${this.name} moved ${distanceInMeters}m.`);
//    }
//}
//class Snake extends Animal {
//    constructor(name: string) { super(name); }
//    move(distanceInMeters = 5) {
//        console.log("Slithering...");
//        super.move(distanceInMeters);
//    }
//}
//class Horse extends Animal {
//    constructor(name: string) { super(name); }
//    move(distanceInMeters = 45) {
//        console.log("Galloping...");
//        super.move(distanceInMeters);
//    }
//}
//let sam = new Snake("Sammy the Python");
//let tom: Animal = new Horse("Tommy the Palomino");
//sam.move();
//tom.move(34);




class Animal {
    private name: string;
    constructor(theName: string) { this.name = theName; }
}

class Rhino extends Animal {
    constructor() { super("Rhino"); }
}

class Employee {
    private name: string;
    constructor(theName: string) { this.name = theName; }
}

let animal = new Animal("Goat");
let rhino = new Rhino();
let employee = new Employee("Goat");

animal = rhino;
//animal = employee; // Error: Animal and Employee are not compatible





class Grid {
    static origin = {x: 0, y: 0};
    calculateDistanceFromOrigin(point: {x: number; y: number;}) {
        let xDist = (point.x - Grid.origin.x);
        let yDist = (point.y - Grid.origin.y);
        return Math.sqrt(xDist * xDist + yDist * yDist) / this.scale;
    }
    constructor (public scale: number) { }
}

let grid1 = new Grid(1.0);  // 1x scale
let grid2 = new Grid(5.0);  // 5x scale

console.log(grid1.calculateDistanceFromOrigin({x: 10, y: 10}));
console.log(grid2.calculateDistanceFromOrigin({x: 10, y: 10}));




function buildName(firstName: string, ...restOfName: string[]) {
  return firstName + " " + restOfName.join(" ");
}
let employeeName = buildName("Joseph", "Samuel", "Lucas", "MacKinzie");





let suits = ["hearts", "spades", "clubs", "diamonds"];

function pickCard(x): any {
    // Check to see if we're working with an object/array
    // if so, they gave us the deck and we'll pick the card
    if (typeof x == "object") {
        let pickedCard = Math.floor(Math.random() * x.length);
        return pickedCard;
    }
    // Otherwise just let them pick the card
    else if (typeof x == "number") {
        let pickedSuit = Math.floor(x / 13);
        return { suit: suits[pickedSuit], card: x % 13 };
    }
}

let myDeck = [{ suit: "diamonds", card: 2 }, { suit: "spades", card: 10 }, { suit: "hearts", card: 4 }];
let pickedCard1 = myDeck[pickCard(myDeck)];
alert("card: " + pickedCard1.card + " of " + pickedCard1.suit);

let pickedCard2 = pickCard(15);
alert("card: " + pickedCard2.card + " of " + pickedCard2.suit);




function create<T>(c: {new(): T; }): T {
    return new c();
}





class BeeKeeper {
    hasMask: boolean;
}

class ZooKeeper {
    nametag: string;
}

class Animal1 {
    numLegs: number;
}

class Bee extends Animal1 {
    keeper: BeeKeeper;
}

class Lion extends Animal1 {
    keeper: ZooKeeper;
}

function findKeeper<A extends Animal1, K> (a: {new(): A;
    prototype: {keeper: K}}): K {

    return a.prototype.keeper;
}

findKeeper(Lion).nametag;  // typechecks!







enum EventType { Mouse, Keyboard }

interface Event { timestamp: number; }
interface MouseEvent extends Event { x1: number; y1: number }
interface KeyEvent extends Event { keyCode: number }

function listenEvent(eventType: EventType, handler: (n: Event) => void) {
    /* ... */
}

// Unsound, but useful and common
listenEvent(EventType.Mouse, (e: MouseEvent) => console.log(e.x1 + ',' + e.y1));

// Undesirable alternatives in presence of soundness
listenEvent(EventType.Mouse, (e: Event) => console.log((<MouseEvent>e).x1 + ',' + (<MouseEvent>e).y1));
listenEvent(EventType.Mouse, <(e: Event) => void>((e: MouseEvent) => console.log(e.x1 + ',' + e.y1)));

// Still disallowed (clear error). Type safety enforced for wholly incompatible types
//listenEvent(EventType.Mouse, (e: number) => console.log(e));
