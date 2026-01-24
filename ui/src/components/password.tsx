import styled from "@emotion/styled";
import React from "react";

export interface PasswordProps {
    label: string;
    error?: string;
}

const Input = styled.input(`
    padding: 6px 8px;
    border: solid 1px #ccc;
`);

const Error = styled.div(`
    color: red;
    font-size: 0.9em;
`);

export const Password = React.forwardRef<HTMLInputElement, PasswordProps & React.InputHTMLAttributes<HTMLInputElement>>(
    ({ label, error, ...props }, ref) => {
        const inputId = React.useId();
        return (
            <>
                <label htmlFor={inputId}>{label}</label>
                <div>
                    <Input id={inputId} type="password" ref={ref} {...props} />
                    {error && <Error>{error}</Error>}
                </div>
            </>
        )
    });