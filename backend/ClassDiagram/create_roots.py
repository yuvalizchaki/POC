import os
import argparse
import re
import subprocess

import logging

# Define the format for the logging
log_format = "%(asctime)s [%(levelname)s] %(message)s"

# Configure the logging
logging.basicConfig(level=logging.DEBUG,
                    format=log_format,
                    datefmt="%Y-%m-%d %H:%M:%S")


def find_files(root_dir, pattern):
    for root, dirs, files in os.walk(root_dir):
        for file in files:
            if pattern.match(file):
                yield os.path.join(root, file)

def extract_class_declarations(content):
    # Regex pattern to match 'class' or 'interface' blocks
    declaration_pattern = r'(class|interface) (\w+)\s*\{'

    # Set to store found class and interface names
    declarations = set()

    # Find all class and interface declarations
    found_declarations = re.findall(declaration_pattern, content, re.MULTILINE)

    # Extract just the class and interface names and add them to the set
    for _, name in found_declarations:
        declarations.add(name)

    return declarations


def extract_classes(content):
    # Regex pattern for valid class names
    pattern = r'\b[A-Z][A-Za-z0-9]*\b'

    classes = set()
    for line in content.split('\n'):
        # Split the line at the first colon and take the part after it
        parts = line.split(':', 1)
        if len(parts) == 2:
            # Apply regex to the part after the colon
            found_classes = re.findall(pattern, parts[1])
            classes.update(found_classes)

    classes.discard('T') # Remove specific names

    return classes

def generate_diagram(jar_path, file_path):
    subprocess.run(["java", "-jar", jar_path, file_path], check=True)

def process_file_recursively(file_path, source_dir, expanded_classes, depth=0):
    if depth > 10:
        return ""

    this_class_name = os.path.basename(file_path).split(".")[0]
    with open(file_path, 'r') as file:
        content = file.read()

    
    # ----- Remove @startuml and @enduml ------
    content_lines = content.split('\n')
    # Remove lines containing '@startuml' or '@enduml'
    content_lines = [line for line in content_lines if '@startuml' not in line and '@enduml' not in line]
    # Join the lines back together
    content = '\n'.join(content_lines)
    # ----- Remove @startuml and @enduml ------
    
    classes_to_expand = extract_classes(content)
    expanded_classes.add(this_class_name)

    additional_content = ""
    
    for other_class_name in classes_to_expand:
        if other_class_name == this_class_name or other_class_name in expanded_classes:
            continue

        target_file = f'{other_class_name}.puml'
        target_path = None
        for root, dirs, files in os.walk(source_dir):
            if target_file in files:
                target_path = os.path.join(root, target_file)
                break
        
        if target_path and os.path.exists(target_path):
            # logging.warning(f'[DEBUG] Finding: {target_path}')
            target_content = process_file_recursively(target_path, source_dir, expanded_classes, depth + 1)
            # ----- Remove existing interfaces/classes if exist ----- 
            # Regex pattern to match 'class' or 'interface' blocks
            class_block_pattern = r'(class|interface) (\w+)\s*\{[^}]*\}'

            # Find all class/interface blocks and their names
            class_blocks = re.findall(class_block_pattern, target_content, re.MULTILINE)

            for block_type, class_name in class_blocks:
                if class_name in classes_to_expand and not other_class_name == class_name:
                    # Remove the class/interface block from target_content
                    target_content = re.sub(fr'{block_type} {class_name}\s*\{{[^}}]*\}}', '', target_content, flags=re.MULTILINE)
            # ----- Remove existing interfaces/classes if exist ----- 
                    
            additional_content += f'\n{this_class_name} ..> {other_class_name}: <<use>>\n{target_content}\n'
        else:
            obj_type = "class" if not other_class_name.startswith('I') else "interface"
            additional_class = f"{obj_type} {other_class_name} {{\n...\n}}\n"
            additional_arrow = f'\n{this_class_name} ..> {other_class_name}: <<use>>\n'
            additional_content += f'{additional_class}\n{additional_arrow}\n'

    if not additional_content == '':
        content += '\n' + additional_content

    if depth == 0:
        content = '@startuml\n' + content + '\n@enduml'
    
    # ----- Remove duplicate arrows ------
    unique_arrows = set()
    filtered_content_lines = []

    for line in content.split('\n'):
        if '..>' in line or '<|--' in line:
            if line not in unique_arrows:
                unique_arrows.add(line)
                filtered_content_lines.append(line)
        else:
            filtered_content_lines.append(line)

    content = '\n'.join(filtered_content_lines)
    # ----- Remove duplicate arrows ------
    
    content = '\n'.join(line for line in content.split('\n') if line.strip()) # remove empty line

    return content

def process_file_wrapper(file_path, source_dir, output_dir, jar_path):
    try:
        logging.info(f'Processing file {file_path}')
        content = process_file_recursively(file_path, source_dir, set(), depth=0)

        # Generate file
        relative_path = os.path.relpath(file_path, source_dir)
        output_file_path = os.path.join(output_dir, relative_path)
        os.makedirs(os.path.dirname(output_file_path), exist_ok=True)
        with open(output_file_path, 'w') as output_file:
            output_file.write(content)
        generate_diagram(jar_path, output_file_path)

    except Exception as e:
        logging.error(f'Error parsing file {file_path}')
        logging.debug(e)

def main():
    parser = argparse.ArgumentParser(description='Process PlantUML files with recursive connections.')
    parser.add_argument('-i', '--input', required=True, help='Input file containing list of root classes.')
    parser.add_argument('-s', '--source', required=True, help='Source directory for PlantUML files.')
    parser.add_argument('-o', '--output', required=True, help='Output directory for processed PlantUML files.')
    parser.add_argument('-r', '--runtime', required=True, help='Path to the PlantUML JAR file.')
    args = parser.parse_args()

    os.makedirs(args.output, exist_ok=True)
    
    with open(args.input, 'r') as input_file:
        lines = input_file.readlines()

    for line in lines:
        line = line.strip()
        if line.endswith('*'):
            dir_path = os.path.join(args.source, line[:-2])
            for file in find_files(dir_path, re.compile(r'.*\.puml')):
                process_file_wrapper(file, args.source, args.output, args.runtime)
        else:
            file_path = os.path.join(args.source, line)
            if os.path.exists(file_path):
                process_file_wrapper(file_path, args.source, args.output, args.runtime)

if __name__ == '__main__':
    main()
