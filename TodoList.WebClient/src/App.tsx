import { CreateTask } from "./crud/create-task";
import { AllTasksList } from "./crud/all-tasks-list";

function App() {
  return (
    <>
      <AllTasksList />
      <CreateTask />
    </>
  );
}

export default App;
