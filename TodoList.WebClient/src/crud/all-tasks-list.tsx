import { useMutation, useQuery, useQueryClient } from "react-query";
import { httpClient } from "../axios";
import { Flex, Typography } from "antd";

type Task = {
	id: string;
	title: string;
	text: string;
};

export function AllTasksList() {
	const allQuery = useQuery<Task[]>({
		queryKey: ["all-tasks"],
		queryFn: async () => (await httpClient.get("/task/getAll")).data,
	});

	const renderBody = () => {
		if (!allQuery.data?.length) {
			return <Typography.Text>Нет задач</Typography.Text>;
		}

		return (
			<Flex vertical gap={12}>
				{allQuery.data?.map((x) => {
					return <TaskCardView key={x.id} task={x} />;
				})}
			</Flex>
		);
	};

	return (
		<div>
			<Typography.Title level={2}>Список задач</Typography.Title>
			{renderBody()}
		</div>
	);
}

type TaskCardViewProps = {
	task: Task;
};

function TaskCardView(props: TaskCardViewProps) {
	const queryClient = useQueryClient();

	const deleteMutation = useMutation({
		mutationFn: () => httpClient.get("/task/delete/" + props.task.id),
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: ["all-tasks"],
			});
		},
	});

	return (
		<div>
			<h3>{props.task.title}</h3>
			<p>{props.task.text}</p>
			<button onClick={() => deleteMutation.mutate()}>Удалить задачу</button>
		</div>
	);
}
