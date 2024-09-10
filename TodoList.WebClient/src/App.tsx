import { CreateTask } from "./crud/assignments/create-task";
import { AllTasksList } from "./crud/assignments/all-tasks-list";
import { Flex } from "antd";
import { EmployeesList } from "./crud/employees/employees-list";
import { EmployeeCreation } from "./crud/employees/employee-creation";

function App() {
  return (
    <div>
      <Flex gap={40}>
        <Todo
          todos={[
            "Добавить редактирование задачи",
            "eslint (удаление неиспользуемых импортов)",
            "На одного работника несколько задач, то есть изменить тип связи",
            "Добавить автокомплит по сотрудникам при создании задачи",
            "Добавить вложения к задаче",
            "Добавить валидацию",
            "Добавить авторизацию",
            "При удалении задачи разрывать связь с работником, employee.taskId = null",
          ]}
        />
        <CreateTask />
        <AllTasksList />
      </Flex>
      <hr />
      <EmployeesList />
      <EmployeeCreation />
    </div>
  );
}

function Todo(props: { todos: string[] }) {
  return (
    <div style={{ width: 400 }}>
      {props.todos.map((x) => (
        <li key={x}>{x}</li>
      ))}
    </div>
  );
}

export default App;
