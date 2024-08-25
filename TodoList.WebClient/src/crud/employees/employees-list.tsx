import { Typography, Card, Flex, Button } from "antd";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { httpClient } from "../../axios";

type Employee = {
	id: string;
	name: string;
};

export function EmployeesList() {
	const queryClient = useQueryClient();

	const employeeQuery = useQuery<Employee[]>({
		queryKey: ["employees"],
		queryFn: async () => (await httpClient.get("/employee/getAll")).data,
	});

	const deleteMutation = useMutation({
		mutationFn: (id: string) => httpClient.get("/employee/delete/" + id),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ["employees"] });
		},
	});

	return (
		<div>
			<Typography.Title>Работники</Typography.Title>
			<Flex style={{ display: "inline-flex" }} vertical>
				{!employeeQuery.data?.length && (
					<Typography.Text>Нет работников</Typography.Text>
				)}
				{employeeQuery.data?.map((x) => {
					return (
						<Card key={x.id}>
							<Flex vertical gap={12}>
								<Typography.Text>{x.name}</Typography.Text>
								<Typography.Text type="secondary">{x.id}</Typography.Text>
								<Button
									type="dashed"
									onClick={() => deleteMutation.mutate(x.id)}
								>
									Удалить
								</Button>
							</Flex>
						</Card>
					);
				})}
			</Flex>
		</div>
	);
}
