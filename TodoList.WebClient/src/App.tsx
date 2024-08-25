import { CreateTask } from "./crud/create-task";
import { AllTasksList } from "./crud/all-tasks-list";
import { Flex } from "antd";

function App() {
	return (
		<Flex gap={40}>
			<CreateTask />
			<AllTasksList />
		</Flex>
	);
}

export default App;
