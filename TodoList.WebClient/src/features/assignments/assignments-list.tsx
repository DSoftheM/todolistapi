import { useMutation, useQuery, useQueryClient } from "react-query"
import { httpClient } from "../../axios"
import { Button, Card, Dropdown, Flex, Input, Select, Space, Typography } from "antd"
import { useEmployeesList } from "../employees/use-employees-list"
import { useImmer } from "use-immer"
import { produce } from "immer"
import { useState } from "react"
import { CheckCircleTwoTone, DownOutlined } from "@ant-design/icons"
import { Link } from "react-router-dom"
import { Nav } from "../../nav"
import { Assignment, AssignmentPriority, assignmentPriorityToString } from "./types/assignment"

function filterByToString(type: FilterBy) {
    switch (type) {
        case FilterBy.CreationDate:
            return "Дата создания"
        case FilterBy.Priority:
            return "Приоритет"
    }
}

enum FilterBy {
    Priority = "priority",
    CreationDate = "creationDate",
}

export function TasksList() {
    const [term, setTerm] = useState("")
    const [filterBy, setFilterBy] = useState(FilterBy.CreationDate)

    const allQuery = useQuery<Assignment[]>({
        queryKey: ["all-tasks", term, filterBy],
        queryFn: async () => {
            const queryParams = new URLSearchParams()
            queryParams.append("term", term)
            queryParams.append("filterBy", filterBy)
            return (await httpClient.get("/task/getAll?" + queryParams)).data
        },
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
                        placeholder="Поиск по названию или описанию"
                        variant="borderless"
                        onChange={(e) => setTerm(e.target.value)}
                    />
                    <div>
                        <Typography.Title level={5}>Фильтр</Typography.Title>
                        <Select
                            value={filterBy}
                            allowClear
                            style={{ width: "100%" }}
                            placeholder="Фильтр"
                            onChange={(_ids, items) => {
                                setFilterBy(_ids)
                            }}
                            options={Object.values(FilterBy).map((key) => {
                                return { label: filterByToString(key), value: key }
                            })}
                        />
                    </div>
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
                    <Dropdown
                        menu={{
                            items: [
                                {
                                    key: AssignmentPriority.Low,
                                    label: assignmentPriorityToString(AssignmentPriority.Low),
                                    onClick: () => {
                                        updateEdit((draft) => {
                                            if (!draft) return
                                            draft.priority = AssignmentPriority.Low
                                        })
                                    },
                                },
                                {
                                    key: AssignmentPriority.Medium,
                                    label: assignmentPriorityToString(AssignmentPriority.Medium),
                                    onClick: () => {
                                        updateEdit((draft) => {
                                            if (!draft) return
                                            draft.priority = AssignmentPriority.Medium
                                        })
                                    },
                                },
                                {
                                    key: AssignmentPriority.High,
                                    label: assignmentPriorityToString(AssignmentPriority.High),
                                    onClick: () => {
                                        updateEdit((draft) => {
                                            if (!draft) return
                                            draft.priority = AssignmentPriority.High
                                        })
                                    },
                                },
                            ],
                        }}
                    >
                        <a onClick={(e) => e.preventDefault()}>
                            <Space>
                                {assignmentPriorityToString(edit.priority)}
                                <DownOutlined />
                            </Space>
                        </a>
                    </Dropdown>
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
        <Card style={{ position: "relative" }}>
            {props.assignment.done && (
                <CheckCircleTwoTone
                    style={{ fontSize: 40, position: "absolute", left: -20, top: -20 }}
                    twoToneColor="#52c41a"
                />
            )}
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
                    <Typography.Title level={5}>Приоритет</Typography.Title>
                    <Typography.Text>{assignmentPriorityToString(props.assignment.priority)}</Typography.Text>
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
                <Link to={Nav.assignments.printForm(props.assignment.id)}>
                    <Button>Печатная форма</Button>
                </Link>
            </Flex>
        </Card>
    )
}
