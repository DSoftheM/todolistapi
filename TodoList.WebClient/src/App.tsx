import { CreateTask } from "./crud/assignments/create-task"
import { AllTasksList } from "./crud/assignments/all-tasks-list"
import { Flex } from "antd"
import { EmployeesList } from "./crud/employees/employees-list"
import { EmployeeCreation } from "./crud/employees/employee-creation"

function App() {
    return (
        <div>
            <Flex gap={40}>
                <Todo
                    todos={[
                        "Добавить редактирование задачи",
                        "eslint (удаление неиспользуемых импортов)",
                        "Добавить вложения к задаче",
                        "Добавить валидацию",
                        "Добавить авторизацию",
                    ]}
                />
                <CreateTask />
                <AllTasksList />
            </Flex>
            <hr />
            <EmployeesList />
            <EmployeeCreation />
        </div>
    )
}

function Todo(props: { todos: string[] }) {
    return (
        <div style={{ width: 400 }}>
            {props.todos.map((x) => (
                <li key={x}>{x}</li>
            ))}
        </div>
    )
}

export default App
