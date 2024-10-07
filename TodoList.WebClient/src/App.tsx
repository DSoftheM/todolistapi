import { createBrowserRouter, Link, Outlet, RouterProvider } from "react-router-dom"
import { Nav } from "./nav"
import { TaskPage } from "./features/assignments/task-page"
import { EmployeesPage } from "./features/employees/employees-page"
import { RegistrationForm } from "./features/auth/registration-form"

const router = createBrowserRouter([
    {
        path: Nav.root,
        element: <Layout />,
        children: [
            { path: Nav.assignments.root, element: <TaskPage />, children: [{ path: Nav.assignments.root + "*" }] },
            { path: Nav.employees, element: <EmployeesPage /> },
        ],
    },
    {
        path: "/auth/register",
        element: <RegistrationForm />,
    },
])

function Layout() {
    return (
        <div>
            <div>
                <Link to={Nav.root}>Главная</Link>
                <Link to={Nav.assignments.root}>Задачи</Link>
                <Link to={Nav.employees}>Работники</Link>
            </div>
            <Outlet />
        </div>
    )
}

function App() {
    return <RouterProvider router={router} />
}

export default App
