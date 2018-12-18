var pluginList = [{
    id: '0',
    text: '框架自带',
    value: 'no',
    hasChildren: true,
    isexpand: true,
    complete: true,
    ChildNodes: [
        {
            id: 'learuntree',
            text: '树形插件',
            value: 'learuntree',
            isexpand: false,
            complete: true
        },
         {
             id: 'learunselect',
             text: '选择框',
             value: 'learunselect',
             isexpand: false,
             complete: true
         },
          {
              id: 'learunuserselect',
              text: '人员选择框',
              value: 'learunuserselect',
              isexpand: false,
              complete: true
          }
    ]
}, {
    id: '1',
    text: '第三方集成',
    value: 'no',
    hasChildren: true,
    isexpand: true,
    complete: true,
    ChildNodes: [
         {
             id: 'jfGrid',
             text: '表格(JFGrid)',
             value: 'jfGrid',
             isexpand: false,
             complete: true
         },
         {
             id: 'webUploader',
             text: '上传文件(WebUploader)',
             value: 'webUploader',
             isexpand: false,
             complete: true
         }
    ]
}];