import { theme } from "@root/theme";
import styled from "@emotion/styled";
import type { ReactNode } from "react";

export type ButtonVariant = "button" | "success" | "warning" | "error";

export interface ButtonProps {
    children: ReactNode;
    variant?: ButtonVariant;
}

function getBackgroundColor(variant?: ButtonVariant) {
    switch (variant) {
        case "success":
            return theme.colors.success;
        case "warning":
            return theme.colors.warning;
        case "error":
            return theme.colors.error;
        default:
            return theme.colors.background;
    }
}

function getFontColor(variant?: ButtonVariant) {
    switch (variant) {
        case "success":
            return theme.colors.successText;
        case "warning":
            return theme.colors.warningText;
        case "error":
            return theme.colors.errorText;
        default:
            return theme.colors.textPrimary;
    }
}

export function Button(props: ButtonProps) {
    const ButtonStyled = styled.button`
        background-color: ${getBackgroundColor(props.variant)};
        color: ${getFontColor(props.variant)};
        padding: ${theme.spaces.xsmall};
    `;
    return <ButtonStyled>{props.children}</ButtonStyled>;
}
