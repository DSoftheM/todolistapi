import { Flex } from "antd"
import { CreateTask } from "./create-task"
import { Route, Routes } from "react-router-dom"
import { AssignmentPrintPreview } from "./assignment-print-preview"
import { TasksList } from "./assignments-list"
import { Nav } from "../../nav"

export function TaskPage() {
    return (
        <>
            <Flex gap={20}>
                <CreateTask />
                <Routes>
                    <Route
                        path={`/print-preview/:assignmentId`}
                        // path={Nav.assignments.printForm(":assignmentId")}
                        element={<AssignmentPrintPreview />}
                    />
                    <Route path={Nav.root} element={<TasksList />} />
                </Routes>
            </Flex>
        </>
    )
}
