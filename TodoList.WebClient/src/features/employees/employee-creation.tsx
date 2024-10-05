import { useMutation, useQueryClient } from "react-query";
import { httpClient } from "../../axios";
import { useState } from "react";
import { Button, Input, Typography } from "antd";

export function EmployeeCreation() {
	const [name, setName] = useState("");
	const queryClient = useQueryClient();

	const createMutation = useMutation({
		mutationFn: () => {
			return httpClient.post("/employee/create", { name });
		},
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ["employees"] });
		},
	});

	return (
		<div>
			<label>
				<Typography.Title level={5}>Имя</Typography.Title>
				<Input value={name} onChange={(e) => setName(e.target.value)} />
			</label>
			<Button onClick={() => createMutation.mutate()} type="primary">
				Добавить
			</Button>
		</div>
	);
}
