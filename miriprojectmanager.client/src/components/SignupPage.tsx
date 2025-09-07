import { useState } from "react";
import { cn } from "../lib/utils";
import {
    Label,
    Input,
    Button,
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "../../components/ui";
import axios, { type AxiosResponse } from "axios";
import { setCookie, tokenCookieName } from "./Authentication";

export function SignUpForm({
    className,
    ...props
}: React.ComponentProps<"div">) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    var [arePasswordsIdentical, setArePasswordsIdentical] = useState(false);

    function checkPasswords() {
        var name1 = document.forms[0]["password"].value;
        var name2 = document.forms[0]["re-password"].value;

        if (name1 == name2) {
            setArePasswordsIdentical(true);
        } else {
            setArePasswordsIdentical(false);
        }
    }

    

    return (
        <div className="flex min-h-svh w-full items-center justify-center p-6 md:p-10">
            <div className="w-full max-w-sm">
        <div className={cn("flex flex-col gap-6", className)} {...props}>
            <Card>
                <CardHeader>
                    <CardTitle>Sign up for the Project Manager!</CardTitle>
                </CardHeader>
                <CardContent>
                    <form name="signup"
                        onSubmit={async (e) => {
                            e.preventDefault();
                            try {
                                const response = await axios.post<
                                    any,
                                    AxiosResponse<Record<string, string>>
                                >("https://localhost:7266/api/auth/register", {
                                    username,
                                    password,
                                });
                                console.log(response.data);
                                setCookie(tokenCookieName, response.data.token, 1);
                            } catch (error) {
                                if (error instanceof Error) {
                                    (document.getElementById('SignupErrorLabel') as HTMLLabelElement).innerHTML = error.message;
                                    (document.getElementById('SignupErrorLabel') as HTMLLabelElement).style.visibility = "visible"
                                    console.log(error);
                                }
                            }
                        }}
                    >
                        <div className="flex flex-col gap-6">
                            <div className="grid gap-3">
                                <Label htmlFor="username">Username</Label>
                                <Input
                                    onChange={(e) => setUsername(e.target.value)}
                                    id="username"
                                    type="username"
                                    required
                                />
                                    </div>
                                    <Label id="SignupErrorLabel" style={{ visibility: "hidden", color: "red" }} />
                            <div className="grid gap-3">
                                <div className="flex items-center">
                                    <Label htmlFor="password">Password</Label>
                                </div>
                                        <Input
                                            
                                    onChange={(e) => setPassword(e.target.value)}
                                    id="password"
                                            type="password"
                                            minLength={8}
                                            maxLength={ 30 }
                                    required
                                />
                            </div>
                            <div className="grid gap-3">
                            <div className="flex items-center">
                                <Label htmlFor="password">Re-enter password</Label>
                            </div>
                            <Input
                                onChange={() => checkPasswords()}
                                id="re-password"
                                type="password"
                                required
                                />
                                <Label style={{ visibility: arePasswordsIdentical ? "hidden" : "visible", color: "red" }}>Passwords do not match</Label>
                            </div>
                            <div className="flex flex-col gap-3">
                                <Button type="submit" className="w-full">
                                    Sign up
                                </Button>
                            </div>
                        </div>
                        <div className="mt-4 text-center text-sm">
                            Already have an account?{" "}
                            <a href="/login" className="underline underline-offset-4">
                                Login
                            </a>
                        </div>
                    </form>
                </CardContent>
            </Card>
                </div>
            </div>
        </div>
    );
}
