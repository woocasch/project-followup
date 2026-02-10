import styled from "@emotion/styled";
import { theme } from "@root/theme";
import type { ReactNode } from "react";
import React from "react";

const Dialog = styled.dialog(`
  border: none;
  border-radius: ${theme.spaces.small};
  padding: ${theme.spaces.medium};
  box-shadow: 0px 4px 16px rgba(0, 0, 0, 0.2);
  background-color: ${theme.colors.background};
`);

export interface DialogBoxProps {
  children: ReactNode;
}

export const DialogBox = React.forwardRef<
  HTMLDialogElement,
  DialogBoxProps & React.DialogHTMLAttributes<HTMLDialogElement>>(({ children, ...props }, ref) => {
    return (
      <Dialog ref={ref} {...props}>
        {children}
      </Dialog>
    );
  });
