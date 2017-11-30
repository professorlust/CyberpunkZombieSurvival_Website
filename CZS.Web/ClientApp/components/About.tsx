﻿import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import * as dotnetify from 'dotnetify';

export default class About extends React.Component<any, any> {
    vm: any;

    constructor(props: any) {
        super(props);
        this.vm = dotnetify.react.connect('AboutViewModel', this);
        this.state = {  }
    }
    
    componentWillUnmount() {
        this.vm.$destroy();
    }

    public render() {
        return (
            <div>
                About
            </div>
        );



    }
}
