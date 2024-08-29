import { useState } from "react";
import { useMutation, useQueryClient } from "react-query";
import { httpClient } from "../../axios";
import { Button, Dropdown, Flex, Input, Typography } from "antd";
import { Employee, useEmployeesList } from "../employees/use-employees-list";

export function CreateTask() {
	const qc = useQueryClient();

	const employeesListQuery = useEmployeesList();

	const createMutation = useMutation({
		mutationFn: () =>
			httpClient.post("/task/create", { title, text, employee }),
		onSuccess: () => {
			qc.invalidateQueries({
				queryKey: ["all-tasks"],
			});
		},
	});

	const [title, setTitle] = useState("");
	const [text, setText] = useState("");
	const [employee, setEmployee] = useState<Employee | null>(null);

	return (
		<Flex vertical gap={30} align="flex-start">
			<label>
				<Typography.Title level={5}>Название</Typography.Title>
				<Input
					type="text"
					value={title}
					onChange={(e) => setTitle(e.target.value)}
				/>
			</label>
			<label>
				<Typography.Title level={5}>Описание</Typography.Title>
				<Input
					type="text"
					value={text}
					onChange={(e) => setText(e.target.value)}
				/>
			</label>
			<Dropdown
				menu={{
					items: (employeesListQuery.data ?? []).map((x) => {
						return { key: x.id, label: x.name, onClick: () => setEmployee(x) };
					}),
				}}
			>
				<a onClick={(e) => e.preventDefault()}>
					{employee?.name ?? "Не выбран"}
				</a>
			</Dropdown>
			<Button onClick={() => createMutation.mutate()}>Создать задачу</Button>
		</Flex>
	);
}
