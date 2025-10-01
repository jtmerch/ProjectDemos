import React, { useState } from 'react';
import './App.css';
import InputField from "./components/InputField";
import { Todo } from "./model";
import TodoList from "./components/TodoList";

let name: string;
//let name: any; //any means no restriction 
//let name: unknown; //unknown also means no restriction this is recommended instead of 'any;
let age: number | string; //can accept number or string
age = 5;

let isStudent: boolean;
let hobbies: string[];

let role:[number, string] //this means there can be a fixed amount of values/types
//role=[5,"rolestr"];

//defining a function:
let printName: (name: string) => void; //void returns "undefined"
//let printName: (name: string) => never; //never doesn't return anything.


//OBJECT:
/*type Person = {
  name: string;
  age: number; //doing age?: means it is optional
};
let person: Person = {
  name: 'Joe',
  age: 45,
}; //object;

let lotsofPeople: Person[];
*/

//difference between "type and interface is type can be extended with other types by "Type: Type while interfaces is easier, yo just use interface "extends" interface "
interface Person {
  name: string;
  age: number; //doing age?: means it is optional
};
/*interface Guy extends Person{
  profession: string;
}
*/


//React.FC is a functional component type React.ReactNode supports all of the types of components
const App: React.FC = () => {

  const [todo, setTodo] = useState<string>("");
  const [todos, setTodos] = useState<Todo[]>([]);

  const handleAdd = (e: React.FormEvent) => {
e.preventDefault();

if(todo)
{
  //...variable means add to what is already there
setTodos([...todos, {id: Date.now(), todo: todo, isDone: false}]);
setTodo("");
}
  };

  return (
    <div className="App">
    <span className="heading">Taskify</span>
    <InputField todo={todo} setTodo={setTodo} handleAdd={handleAdd} />
 <TodoList todos={todos} setTodos={setTodos} />
    </div>
  );
}

export default App;
