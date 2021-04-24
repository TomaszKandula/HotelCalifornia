import ReactHtmlParser from "react-html-parser";
import { Modal, Button } from "react-bootstrap"
import { IconType } from "../Shared/enums";
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
            case IconType.info: return(<BsFillInfoCircleFill />);
            case IconType.warning: return(<BsExclamationTriangleFill />);
            case IconType.error: return(<BsFillExclamationCircleFill />);
            default: return(<BsFillInfoCircleFill />);
        }        
    };   

    return (
        <Modal show={props.state} onHide={props.handle} backdrop="static" keyboard={false} >
            <Modal.Header closeButton>
                <RenderIcon />
                <Modal.Title>
                    {ReactHtmlParser(props.title)}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
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