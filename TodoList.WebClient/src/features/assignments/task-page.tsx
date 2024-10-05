import { Flex } from "antd"
import { CreateTask } from "./create-task"
import { TasksList } from "./tasks-list"

export function TaskPage() {
    return (
        <>
            <Flex gap={20}>
                <CreateTask />
                <TasksList />
            </Flex>
        </>
    )
}
