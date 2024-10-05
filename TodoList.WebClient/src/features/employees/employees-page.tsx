import { Flex } from "antd"
import { EmployeeCreation } from "./employee-creation"
import { EmployeesList } from "./employees-list"

export function EmployeesPage() {
    return (
        <Flex gap={20}>
            <EmployeeCreation />
            <EmployeesList />
        </Flex>
    )
}
