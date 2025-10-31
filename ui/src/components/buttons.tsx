import { theme } from "@root/theme";
import styled from "@emotion/styled";

export interface RedirectButtonProps {
    title: string;
}

export function RedirectButton(props: RedirectButtonProps) {
    const { title } = props;
    const RedirectButtonStyle = styled.input(`
        background-color: ${theme.colors.accent};
        `);
    return <RedirectButtonStyle type="button" value={title} />;
}