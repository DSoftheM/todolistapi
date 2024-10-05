import { useMutation, useQuery, useQueryClient } from "react-query"
import { httpClient } from "../../axios"
import { Button, Card, Flex, Input, Select, Typography } from "antd"
import { Employee, useEmployeesList } from "../employees/use-employees-list"
import { useImmer } from "use-immer"

type Task = {
    id: string
    title: string
    text: string
    created: Date
    employees: Employee[]
}

export function AllTasksList() {
    const allQuery = useQuery<Task[]>({
        queryKey: ["all-tasks"],
        queryFn: async () => (await httpClient.get("/task/getAll")).data,
    })

    const renderBody = () => {
        if (!allQuery.data?.length) {
            return <Typography.Text>Нет задач</Typography.Text>
        }

        return (
            <Flex vertical gap={12}>
                {allQuery.data?.map((x) => {
                    return <TaskCardView key={x.id} task={x} />
                })}
            </Flex>
        )
    }

    return (
        <div>
            <Typography.Title level={2}>Список задач</Typography.Title>
            {renderBody()}
        </div>
    )
}

type TaskCardViewProps = {
    task: Task
}

function TaskCardView(props: TaskCardViewProps) {
    const queryClient = useQueryClient()

    const deleteMutation = useMutation({
        mutationFn: () => httpClient.get("/task/delete/" + props.task.id),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["all-tasks"],
            })
        },
    })

    const [edit, updateEdit] = useImmer<Task | null>(null)
    const employeesListQuery = useEmployeesList()

    const editMutation = useMutation({
        mutationFn: () => httpClient.post("/task/edit", edit),
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
                    <Typography.Text>{new Date(props.task.created).toLocaleString()}</Typography.Text>
                    <Button
                        onClick={() => {
                            editMutation.mutate()
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
                    <Typography.Text>{props.task.title}</Typography.Text>
                </div>
                <div>
                    <Typography.Title level={5}>Описание</Typography.Title>
                    <Typography.Text>{props.task.text}</Typography.Text>
                </div>
                <div>
                    <Typography.Title level={5}>Ответственные</Typography.Title>
                    <Flex vertical gap={8} component="ul">
                        {props.task.employees.map((e) => {
                            return (
                                <li key={e.id}>
                                    <Typography.Text>{e.name}</Typography.Text>
                                </li>
                            )
                        })}
                    </Flex>
                </div>
                <Typography.Text type="secondary">id = {props.task.id}</Typography.Text>
                <Typography.Text>{new Date(props.task.created).toLocaleString()}</Typography.Text>
                <Button onClick={() => deleteMutation.mutate()}>Удалить</Button>
                <Button onClick={() => updateEdit(props.task)}>Редактировать</Button>
            </Flex>
        </Card>
    )
}
