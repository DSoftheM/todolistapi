import { useState } from "react"
import { useMutation, useQueryClient } from "react-query"
import { httpClient } from "../../axios"
import { Button, Flex, Input, Select, Typography } from "antd"
import { Employee, useEmployeesList } from "../employees/use-employees-list"

export function CreateTask() {
    const qc = useQueryClient()

    const employeesListQuery = useEmployeesList()

    const createMutation = useMutation({
        mutationFn: () => httpClient.post("/task/create", { title, text, employees }),
        onSuccess: () => {
            qc.invalidateQueries({
                queryKey: ["all-tasks"],
            })
        },
    })

    const [title, setTitle] = useState("")
    const [text, setText] = useState("")
    const [employees, setEmployees] = useState<Employee[] | null>(null)

    return (
        <Flex vertical gap={30} style={{ maxWidth: 400, width: "100%" }}>
            <label>
                <Typography.Title level={5}>Название</Typography.Title>
                <Input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
            </label>
            <label>
                <Typography.Title level={5}>Описание</Typography.Title>
                <Input type="text" value={text} onChange={(e) => setText(e.target.value)} />
            </label>
            <label>
                <Typography.Title level={5}>Ответственные</Typography.Title>
                <Select
                    mode="multiple"
                    value={employees?.map((x) => x.id)}
                    allowClear
                    style={{ width: "100%" }}
                    placeholder="Выберите ответственных"
                    onChange={(_ids, items) => {
                        if (Array.isArray(items)) setEmployees(items.map((x) => x.data))
                    }}
                    options={(employeesListQuery.data ?? []).map((x) => {
                        return { label: x.name, value: x.id + x.name, data: x }
                    })}
                />
            </label>
            <Button onClick={() => createMutation.mutate()}>Создать задачу</Button>
        </Flex>
    )
}
