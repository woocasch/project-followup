import styled from "@emotion/styled";
import { theme } from '../theme';

const StyledFooter = styled.footer(`
    width: 100%;
    line-height: ${theme.colors.spacing.xlarge};
    text-align: center;
    background-color: ${theme.colors.background.secondary};
    padding: ${theme.colors.spacing.small};
    margin-top: auto;
`);

export default function Footer(){
    return <StyledFooter>
    Project Follow-Up app supported by <a href="https://lukasznowakowski.it">https://lukasznowakowski.it</a>.
    </StyledFooter>;
}
