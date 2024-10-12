import { Button, Flex, Form, Input, Typography } from "antd"
import { useState } from "react"
import { useRegister } from "./api"

export function RegistrationForm() {
    const [login, setLogin] = useState("")
    const [password, setPassword] = useState("")
    const [confirmPassword, setConfirmPassword] = useState("")

    const registerMutation = useRegister()

    return (
        <Form>
            <Flex vertical gap={10}>
                <label>
                    <Typography.Title level={5}>Логин</Typography.Title>
                    <Input value={login} placeholder="Логин" onChange={(ev) => setLogin(ev.target.value)} />
                </label>
                <label>
                    <Typography.Title level={5}>Пароль</Typography.Title>
                    <Input
                        type="password"
                        value={password}
                        placeholder="Пароль"
                        onChange={(ev) => setPassword(ev.target.value)}
                    />
                </label>
                <label>
                    <Typography.Title level={5}>Повторите пароль</Typography.Title>
                    <Input
                        type="password"
                        value={confirmPassword}
                        placeholder="Повторите пароль"
                        onChange={(ev) => setConfirmPassword(ev.target.value)}
                    />
                </label>
                <Flex gap={12}>
                    <Button onClick={() => registerMutation.mutate({ login, password })}>Зарегистрироваться</Button>
                </Flex>
            </Flex>
        </Form>
    )
}

// type CheckPasswordProps = {
//     password: string
// }

// function CheckPassword(props: CheckPasswordProps) {
//     return <Flex vertical>123</Flex>
// }
