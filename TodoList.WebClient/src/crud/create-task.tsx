import { useState } from "react";
import { useMutation, useQueryClient } from "react-query";
import { httpClient } from "../axios";
import { Button, Flex, Input, Typography } from "antd";

export function CreateTask() {
	const qc = useQueryClient();

	const createMutation = useMutation({
		mutationFn: () => httpClient.post("/task/create", { title, text }),
		onSuccess: () => {
			qc.invalidateQueries({
				queryKey: ["all-tasks"],
			});
		},
	});

	const [title, setTitle] = useState("");
	const [text, setText] = useState("");

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
			<Button onClick={() => createMutation.mutate()}>Создать задачу</Button>
		</Flex>
	);
}
