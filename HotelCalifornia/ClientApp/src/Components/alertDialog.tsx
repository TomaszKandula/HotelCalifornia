import ReactHtmlParser from "react-html-parser";
import { Modal, Button } from "react-bootstrap"
import { IconType } from "../Shared/enums";
import { CustomColours } from "../Theme/customColours";
import { 
    BsFillInfoCircleFill, 
    BsExclamationTriangleFill, 
    BsFillExclamationCircleFill 
} from "react-icons/bs";

export interface IAlertModal
{
    state: boolean;
    title: string; 
    message: string; 
    icon: IconType;
}

export interface IAlertDialog extends IAlertModal
{
    handle: any
}

export const alertModalDefault: IAlertModal = 
{
    state: false, 
    title:  "", 
    message: "", 
    icon: IconType.info
}

export function AlertDialog(props: IAlertDialog) 
{
    const RenderIcon = () => 
    {
        switch (props.icon)
        {
            case IconType.info: return(<BsFillInfoCircleFill color={CustomColours.alerts.info} size="36px" />);
            case IconType.warning: return(<BsExclamationTriangleFill color={CustomColours.alerts.warning} size="36px" />);
            case IconType.error: return(<BsFillExclamationCircleFill color={CustomColours.alerts.danger} size="36px" />);
            default: return(<BsFillInfoCircleFill color={CustomColours.alerts.info} size="36px" />);
        }        
    };   

    return (
        <Modal show={props.state} onHide={props.handle} backdrop="static" keyboard={false} >
            <Modal.Header closeButton>
                <RenderIcon />
                <Modal.Title style={{ marginLeft: "15px" }}>
                    {ReactHtmlParser(props.title)}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body style={{ color: CustomColours.typography.gray2 }}>
                {ReactHtmlParser(props.message)}
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={props.handle}>
                    OK
                </Button>
            </Modal.Footer>
        </Modal>
    );
}