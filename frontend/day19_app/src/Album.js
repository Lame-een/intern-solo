import { useEffect } from "react";

export default function Album({callBack}){

    useEffect(()=>{
        callBack("Album");
    }, [callBack]);

    return(
        <div>
            <h1>ALBUM</h1>
        </div>
    );
}