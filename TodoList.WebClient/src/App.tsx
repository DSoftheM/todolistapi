import { CreateTask } from "./features/assignments/create-task"
import { TasksList } from "./features/assignments/tasks-list"
import { Flex } from "antd"
import { EmployeesList } from "./features/employees/employees-list"
import { EmployeeCreation } from "./features/employees/employee-creation"
import { createBrowserRouter, Link, Outlet, RouterProvider } from "react-router-dom"
import { Nav } from "./nav"
import { TaskPage } from "./features/assignments/task-page"
import { EmployeesPage } from "./features/employees/employees-page"

const router = createBrowserRouter([
    {
        path: "/",
        element: <Layout />,
        children: [
            // { index: true, element: <Main /> },
            { path: Nav.assignments, element: <TaskPage /> },
            { path: Nav.employees, element: <EmployeesPage /> },
        ],
    },
])

function Layout() {
    return (
        <div>
            <div>
                <Link to={Nav.main}>Главная</Link>
                <Link to={Nav.assignments}>Задачи</Link>
                <Link to={Nav.employees}>Работники</Link>
            </div>
            <Outlet />
        </div>
    )
}

function Main() {
    return (
        <div>
            <Flex gap={40}>
                {/* <Todo
            todos={[
                "Добавить редактирование задачи",
                "eslint (удаление неиспользуемых импортов)",
                "Добавить вложения к задаче",
                "Добавить валидацию",
                "Добавить авторизацию",
            ]}
        /> */}
                <CreateTask />
                <TasksList />
            </Flex>
            <hr />
            <EmployeesList />
            <EmployeeCreation />
        </div>
    )
}

function App() {
    return <RouterProvider router={router} />
}

export default App
