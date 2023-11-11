import { FC } from "react";
import cx from "classnames"
import "../styles/Button.sass"

type ButtonActionProp = { type?: "outlined" | "contained" | "text" } & React.HTMLProps<HTMLButtonElement>;

export const Button: FC<ButtonActionProp> = ({
    type = "outlined",
    className,
    ...buttonProps
}) => {

    return (
        <button
            type="button"
            {...buttonProps}
            className={cx("button-action", { [`button-action--${type}`]: type },
                `${className}`)}
        >
        </button >
    );
};
