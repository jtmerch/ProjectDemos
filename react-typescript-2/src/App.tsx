import React, {FC, createContext} from 'react';
import './App.css';
import { Person } from './components/Person';
import { HairColor } from './components/Enums';
 /* 
function App() {

 const name: string = "Joe";
  const age: number = 45;
  const isMarried: boolean = false;

  const getName = (name: string): number => {
    if (name == "Joe")
    {
      return 20;
    }
    else
    {
      return 0
    }
  }

  return (
    <div className="App">

    </div>
  );
}
*/

interface AppContextInterface {
  name?: string; //Question mark makes property optional
  age: number;
  country: string;
 // getName: (name: string) => string; //defining a function in interface
}

const AppContext = createContext<AppContextInterface | null>(null)

const App: FC = () => { //functional component way to create APP
  
  const contextValue: AppContextInterface = {
    name: "Joe",
    age: 45,
    country: "US"
  }
  return (
    <AppContext.Provider value={contextValue}>
         <div className="App">
<Person name="Joe" age={45} email="jtmerch@gmail.com" hairColor={HairColor.Black} />
    </div>
    </AppContext.Provider>

    /*
    <div className="App">
<Person name="Joe" age={45} email="jtmerch@gmail.com" hairColor={HairColor.Black} />
    </div>
    */
  );
}
export default App;
