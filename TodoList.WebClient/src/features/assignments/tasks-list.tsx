import { useMutation, useQuery, useQueryClient } from "react-query"
import { httpClient } from "../../axios"
import { Button, Card, Flex, Input, Select, Typography } from "antd"
import { Employee, useEmployeesList } from "../employees/use-employees-list"
import { useImmer } from "use-immer"
import { produce } from "immer"
import { useState } from "react"

type Assignment = {
    id: string
    title: string
    text: string
    created: Date
    employees: Employee[]
    done: boolean
}

export function TasksList() {
    const [term, setTerm] = useState("")

    const allQuery = useQuery<Assignment[]>({
        queryKey: ["all-tasks", term],
        queryFn: async () => (await httpClient.get("/task/getAll?term=" + term)).data,
    })

    const renderBody = () => {
        if (!allQuery.data?.length) {
            return <Typography.Text>Нет задач</Typography.Text>
        }

        return (
            <Flex gap={12}>
                {allQuery.data?.map((x) => {
                    return <TaskCardView key={x.id} assignment={x} />
                })}
            </Flex>
        )
    }

    return (
        <div>
            <Flex vertical gap={20}>
                <div>
                    <Typography.Title level={2}>Список задач</Typography.Title>
                    <Input
                        type="text"
                        value={term}
                        placeholder="Поиск"
                        variant="borderless"
                        onChange={(e) => setTerm(e.target.value)}
                    />
                </div>
                {renderBody()}
            </Flex>
        </div>
    )
}

type TaskCardViewProps = {
    assignment: Assignment
}

function TaskCardView(props: TaskCardViewProps) {
    const queryClient = useQueryClient()

    const deleteMutation = useMutation({
        mutationFn: () => httpClient.get("/task/delete/" + props.assignment.id),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["all-tasks"],
            })
        },
    })

    const [edit, updateEdit] = useImmer<Assignment | null>(null)
    const employeesListQuery = useEmployeesList()

    const editMutation = useMutation({
        mutationFn: (assignment: Assignment) => httpClient.post("/task/edit", assignment),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["all-tasks"],
            })
        },
    })

    if (edit) {
        return (
            <Card>
                <Flex vertical gap={12}>
                    <div>
                        <Typography.Title level={5}>Название</Typography.Title>
                        <Input
                            value={edit.title}
                            onChange={(ev) => {
                                updateEdit((draft) => {
                                    if (!draft) return
                                    draft.title = ev.target.value
                                })
                            }}
                        />
                    </div>
                    <div>
                        <Typography.Title level={5}>Текст</Typography.Title>
                        <Input
                            value={edit.text}
                            onChange={(ev) => {
                                updateEdit((draft) => {
                                    if (!draft) return
                                    draft.text = ev.target.value
                                })
                            }}
                        />
                    </div>
                    <Flex vertical>
                        <Typography.Title level={5}>Ответственные</Typography.Title>
                        <Select
                            mode="multiple"
                            value={edit.employees.map((x) => x.id)}
                            allowClear
                            style={{ width: "100%" }}
                            placeholder="Выберите ответственных"
                            onChange={(_ids, items) => {
                                if (Array.isArray(items)) {
                                    updateEdit((draft) => {
                                        if (!draft) return
                                        draft.employees = items.map((x) => x.data)
                                    })
                                }
                            }}
                            options={(employeesListQuery.data ?? []).map((x) => {
                                return { label: x.name, value: x.id, data: x }
                            })}
                        />
                    </Flex>
                    <Typography.Text>{new Date(props.assignment.created).toLocaleString()}</Typography.Text>
                    <Button
                        onClick={() => {
                            editMutation.mutate(edit)
                            updateEdit(null)
                        }}
                    >
                        Сохранить
                    </Button>
                    <Button onClick={() => updateEdit(null)}>Отмена</Button>
                </Flex>
            </Card>
        )
    }

    return (
        <Card>
            <Flex vertical gap={12}>
                <div>
                    <Typography.Title level={5}>Название</Typography.Title>
                    <Typography.Text>{props.assignment.title}</Typography.Text>
                </div>
                <div>
                    <Typography.Title level={5}>Описание</Typography.Title>
                    <Typography.Text>{props.assignment.text}</Typography.Text>
                </div>
                <div>
                    <Typography.Title level={5}>Ответственные</Typography.Title>
                    <Flex vertical gap={8} component="ul">
                        {props.assignment.employees.map((e) => {
                            return (
                                <li key={e.id}>
                                    <Typography.Text>{e.name}</Typography.Text>
                                </li>
                            )
                        })}
                    </Flex>
                </div>
                <Typography.Text type="secondary">id = {props.assignment.id}</Typography.Text>
                <Flex vertical>
                    <Typography.Title level={5}>Создана</Typography.Title>
                    <Typography.Text>{new Date(props.assignment.created).toLocaleString()}</Typography.Text>
                </Flex>
                <Button onClick={() => deleteMutation.mutate()}>Удалить</Button>
                {props.assignment.done ? (
                    <Button
                        onClick={() => {
                            const updated = produce(props.assignment, (draft) => {
                                draft.done = false
                            })
                            editMutation.mutate(updated)
                        }}
                    >
                        Не выполнена
                    </Button>
                ) : (
                    <Button
                        onClick={() => {
                            const updated = produce(props.assignment, (draft) => {
                                draft.done = true
                            })
                            editMutation.mutate(updated)
                        }}
                    >
                        Выполнена
                    </Button>
                )}

                <Button onClick={() => updateEdit(props.assignment)}>Редактировать</Button>
            </Flex>
        </Card>
    )
}
